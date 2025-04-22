# Generate a pdf representation for all the files with a branching node
import sys
sys.path.append(".")
import unity_parser
import os
import json
import networkx as nx
import matplotlib.pyplot as plt

def visualize_graph(fileName, graph_data):
    G = nx.DiGraph()
    pos = {}
    # Add nodes
    for node in graph_data["nodes_with_ID"]:
        G.add_node(node['$id'], label=node['$type'].removeprefix("Unity.VisualScripting."))
        pos[node['$id']] = (node['position']['x'], -node['position']['y'])
    # Add edges
    for edge in graph_data["edges"]:
        G.add_edge(edge["source"], edge["destination"], edge="g", weight=1)
    # Visualize the graph
    nx.draw(G, pos, with_labels=False, node_size=100, node_color="lightblue", arrows=True)
    labels = nx.get_node_attributes(G, 'label')
    nx.draw_networkx_labels(G, pos, labels=labels, font_size=3, font_color="black")
    plt.savefig(f"{fileName}.pdf")
    plt.close()

directory = os.path.abspath("./files/")

branch_keywords = [
    "Unity.VisualScripting.Switch",
    "Unity.VisualScripting.While",
    "Unity.VisualScripting.If",
    "Unity.VisualScripting.For"
]

file_pathes_with_conditional = {}

for file in os.listdir(directory):
    file_path = os.path.join(directory, file)
    if os.path.isfile(file_path):  
        # print(f"Processing {file_path}")
        try:
            ret = unity_parser.parse_unity_asset(file_path) 
            for node in ret["nodes_with_ID"]:
                if node['$type'] in branch_keywords:
                    print(f"Branching node found in {file_path}")
                    # file_pathes_with_conditional.add(file_path)
                    file_pathes_with_conditional[file_path] = ret
                    break
        except Exception as e: 
            print(f"Error processing {file_path}: {e}") 

output_directory = os.path.abspath("./parsed_graphs/")
os.makedirs(output_directory, exist_ok=True)

for key, value in file_pathes_with_conditional.items():
    fileName = os.path.join(output_directory, os.path.basename(key))
    print(f"Visualizing {fileName}")
    visualize_graph(fileName, value)


