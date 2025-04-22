import json
import networkx as nx
import matplotlib.pyplot as plt
import sys
import os

sys.path.append(os.path.join(os.path.dirname(__file__), '.'))

with open('./blueprint_graphs_Uber.json', 'r') as f:
    blueprint_graph_data = json.load(f)


def visualize_graph(graph_data):
    G = nx.DiGraph()
    pos = {}
    knots = set()
    # Add nodes
    for node in graph_data["nodes"]:
        print(node)
        if node[1] != 'Knot':
            G.add_node(node[0], label=node[1])
            if node[2] is not None and node[3] is not None:
                pos[node[0]] = (node[2], node[3])
            else:
                pos[node[0]] = (0, 0)
        else:
            knots.add(node[0])

    # Add edges
    for edge in graph_data["edges"]:
        if edge[0] not in knots and edge[3] == "EGPD_Output" and edge[1] not in knots:
        # if(edge[1] == "EGPD_Output"):
            G.add_edge(edge[0], edge[1])


    # Visualize the graph
    pos = nx.spring_layout(G)  # positions for all nodes
    nx.draw(G, pos, with_labels=False, node_size=500, node_color="lightblue", arrows=True)
    labels = nx.get_node_attributes(G, 'label')
    nx.draw_networkx_labels(G, pos, labels=labels, font_size=10, font_color="black")
    plt.show()


def main():
    import argparse
    result = blueprint_graph_data["ba1396f7-2df2-4235-baca-233a96e0d05d"]
    # print(result)
    if result:
        visualize_graph(result)

if __name__ == "__main__":
    main()
