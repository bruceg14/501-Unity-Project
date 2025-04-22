# Developed while researching for the Unity Visual Scripting (UVS) file format for Professor Hindle

import yaml
import json
from pathlib import Path

def preprocess_unity_file(content):
    """Preprocess Unity asset file content by removing specific tags."""
    return content.replace('!u!114 &11400000', '')

def parse_graph_elements(graph_data):
    """Parse graph elements into nodes and edges."""
    nodes_with_ID = []
    nodes_without_ID = []
    edges = []
    id_count = 0

    for obj in graph_data['graph']['elements']:
        if obj['$type'] not in ['Unity.VisualScripting.ControlConnection', 'Unity.VisualScripting.ValueConnection']:
            if "$id" in obj:
                id_count += 1
                nodes_with_ID.append(obj)
                # nodes_with_ID[obj["$id"]] = obj
            else:
                nodes_without_ID.append(obj)
                # nodes_without_ID[obj["guid"]] = obj
        else:
            edges.append(obj)

    processed_edges = [
        {
            "source": edge["sourceUnit"]["$ref"],
            "destination": edge["destinationUnit"]["$ref"],
            "type": edge["$type"],
            "sourceKey": edge["sourceKey"] if "sourceKey" in edge else None,
            "destinationKey": edge["destinationKey"] if "destinationKey" in edge else None,
        } for edge in edges
    ]

    return nodes_with_ID, nodes_without_ID, processed_edges, id_count

def parse_unity_asset(file_path):
    """Main function to parse Unity asset file."""
    try:
        with open(file_path, 'r') as file:
            raw_content = file.read()

        preprocessed_content = preprocess_unity_file(raw_content)
        
        try:
            data = yaml.safe_load(preprocessed_content)
            json_string = data['MonoBehaviour']['_data']['_json']
            graph_data = json.loads(json_string)
            
            nodes_with_ID, nodes_without_ID, edges, id_count = parse_graph_elements(graph_data)
            # two_nodes_subgraph = create_two_node_subgraphs(edges, nodes_with_ID)

            ast = {
                "num_nodes": len(nodes_with_ID) + len(nodes_without_ID),
                "num_edges": len(edges),
                "id_count": id_count,
                "nodes_with_ID": nodes_with_ID,
                "nodes_without_ID": nodes_without_ID,
                "edges": edges
            }
            
            return ast

        except yaml.YAMLError as e:
            print(f"Error parsing YAML: {e}")
            return None
        except KeyError as e:
            print(f"Error accessing data structure: {e}")
            return None
        except json.JSONDecodeError as e:
            print(f"Error parsing JSON: {e}")
            return None

    except FileNotFoundError:
        print(f"File not found: {file_path}")
        return None

def main():
    """Main entry point with command line argument handling."""
    import argparse
    
    parser = argparse.ArgumentParser(description='Parse Unity asset files')
    parser.add_argument('file_path', help='Path to the Unity asset file')
    parser.add_argument('--output', '-o', help='Output file path (optional)')
    
    args = parser.parse_args()
    
    ast = parse_unity_asset(args.file_path)
    
    if ast:
        if args.output:
            with open(args.output, 'w') as f:
                json.dump(ast, f, indent=4)
        else:
            print("AST here:")
            print(json.dumps(ast, indent=4))

if __name__ == "__main__":
    main()