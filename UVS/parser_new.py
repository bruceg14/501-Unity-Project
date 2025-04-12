import sys
sys.path.append(".")
import unity_parser
import os
import json
import networkx as nx
import matplotlib.pyplot as plt


class Node:
    def __init__(self, content, branch_type="none", id=0):
        self.content = content
        self.id = id
        self.branch_type = branch_type
        self.children = []

    def add_child(self, child):
        self.children.append(child)

    def get_children(self):
        return self.children

    def get_branch_type(self):
        return self.branch_type
    
    def get_content(self):
        return self.content

    def __repr__(self):
        return f"Node({self.id}, {self.content}, {self.branch_type})"

class CFG_Node:
    def __init__(self, id, content):
        self.id = id
        self.content = content
    def __repr__(self):
        return f"Node{self.content}"


class CFG_Edge:
    def __init__(self, source, target):
        self.source = source
        self.target = target

    def __repr__(self):
        return f"Edge({self.source.id} -> {self.target.id})"
    
class CFG_Graph:
    def __init__(self):
        self.nodes = []
        self.edges = []
        self.curr_id = 0

    def add_node(self, content):
        node = CFG_Node(self.curr_id, content)
        self.curr_id += 1
        self.nodes.append(node)
        return node

    def add_edge(self, source, destination):
        edge = CFG_Edge(source, destination)
        self.edges.append(edge)

    def __repr__(self):
        return f"Graph({len(self.nodes)} nodes, {len(self.edges)} edges)"
    
    def print_graph(self):
        for node in self.nodes:
            print(node)
        for edge in self.edges:
            print(f"{edge.source.id} -> {edge.target.id};")


def recursive(UVS_node, graph, last_node): # UVS_node is a Node class, graph is CFG_graph class, and last_node is CFG_Node class
    cur_node = graph.add_node(UVS_node.get_content())
    graph.add_edge(last_node, cur_node)
    last_node = cur_node
    #Not end node
    while(len(UVS_node.get_children()) > 0):
        print("1231")
        child_nodes = UVS_node.get_children()
        
        if UVS_node.get_content() == "Unity.VisualScripting.If":
            if_node = graph.add_node("If")
            graph.add_edge(last_node, if_node)
            after_if_node = graph.add_node("After_if")
            print("111")
            # last_node = if_node
            if len(child_nodes) == 2:
                print(" i am here")
                for child_node in child_nodes:
                    if child_node.get_branch_type() == "ifTrue":
                        if_true_node = graph.add_node("If_true")
                        graph.add_edge(if_node, if_true_node)
                        after_node = recursive(child_node, graph, if_true_node)
                        graph.add_edge(after_node, after_if_node)
                    else:
                        if_false_node = graph.add_node("If_false")
                        graph.add_edge(if_node, if_false_node)
                        after_node = recursive(child_node, graph, if_false_node)
                        graph.add_edge(after_node, after_if_node)

            elif len(child_nodes) == 1:
                if child_nodes[0].get_branch_type() == "ifTrue":
                    if_true_node = graph.add_node("If_true")
                    graph.add_edge(if_node, if_true_node)
                    after_node = recursive(child_nodes[0], graph, if_true_node)
                    graph.add_edge(after_node, after_if_node)
                else:
                    if_false_node = graph.add_node("If_false")
                    graph.add_edge(if_node, if_false_node)
                    after_node = recursive(child_nodes[0], graph, if_false_node)
                    graph.add_edge(after_node, after_if_node)
                graph.add_edge(if_node, after_if_node)
            return after_if_node
        elif UVS_node.get_content() == "Unity.VisualScripting.For":
            for_node = graph.add_node("For")
            graph.add_edge(last_node, for_node)
            after_for_node = graph.add_node("After_for")
            
            print(child_nodes)
            for child_node in child_nodes:
                if child_node.get_branch_type() == "body":
                    body_node = graph.add_node("For_body")
                    graph.add_edge(for_node, body_node)

                    after_body_node = recursive(child_node, graph, body_node)
                    graph.add_edge(after_body_node, for_node)    
                elif child_node.get_branch_type() == "exit":
                    print("999")
                    graph.add_edge(for_node, after_for_node)
                    # exit_for_node = recursive(child_node, graph, after_for_node)
                    # last_node = after_for_node # Should I add this?
                    UVS_node = child_node
                
            # return after_for_node
            
        else:
            UVS_node = child_nodes[0]
        
    return last_node

    # graph.add_edge(CFG_Edge(last_node, node))
    # last_node = node

    # while(len(node.get_children()) > 0):
    #     child_nodes = node.get_children()
        
    #     if len(child_nodes) > 1:
    #         if(node["$type"] == "Unity.VisualScripting.If"):
    #             graph.add_node(node)
    #             graph.add_edge(CFG_Edge(last_node, node))
    #             for child_node in child_nodes:
    #                 if child_node.get_branch_type() == "ifTrue":
    #                     graph.add_node(child_node)
    #                     recursive(child_node, graph, node)
    #                 else:
    #                     graph.add_node(child_node)
    #                     recursive(child_node, graph, node)
                

                        
    #             None
    #         elif (node["$type"] == "for"):
    #             None
    #         elif (node["$type"] == "while"):
    #             None
    #         elif (node['$type'] == "switch"):
    #             None
    #         else:
    #             None
    #     else:
    #         node = child_nodes[0]
    
    # return last_node


######################################################################################################################

graph_dict = {}

ret = unity_parser.parse_unity_asset("tmp_if.asset")

nodes = ret["nodes_with_ID"]
edges = ret["edges"]

nodes_dict = {node["$id"] : node for node in nodes}

for edge in edges:
    source = edge["source"]
    destination = edge["destination"]

    source_node = nodes_dict[source]
    branch_type = "none"
    # print(source_node)
    if source_node["$type"] == "Unity.VisualScripting.If": 
        if edge["sourceKey"] == "ifTrue":
            branch_type = "ifTrue"
        elif edge["sourceKey"] == "ifFalse":
            branch_type = "ifFalse"
    elif source_node["$type"] == "Unity.VisualScripting.For":
        if edge["sourceKey"] == "body":
            branch_type = "body"
        elif edge["sourceKey"] == "exit":
            branch_type = "exit"
        else:
            branch_type = "current_index"
    elif source_node["$type"] == "Unity.VisualScripting.While":      
        if edge["sourceKey"] == "body":
            branch_type = "body"
        elif edge["sourceKey"] == "exit":
            branch_type = "exit"
    elif source_node["$type"] == "Unity.VisualScripting.Switch":     
        if edge["sourceKey"] == "default":
            branch_type = "default"
        else:
            branch_type = "switch_type"

    if source not in graph_dict:
        graph_dict[source] = Node(nodes_dict[source]["$type"], "none" ,source)
    if destination not in graph_dict:
        graph_dict[destination] = Node(nodes_dict[destination]["$type"], branch_type, destination)
    else:
        if branch_type != "current_index":
            graph_dict[destination].branch_type = branch_type
    graph_dict[source].add_child(graph_dict[destination])

print(graph_dict)


dicts = {}

for node in nodes:
    dicts[node['$id']] = 0
for edge in edges:
    if edge["type"] == "Unity.VisualScripting.ControlConnection" and edge["destination"] in dicts:
        dicts[edge["destination"]] = 1
    if edge["type"] == "Unity.VisualScripting.ValueConnection":
        dicts[edge["source"]] = 1
start_nodes = []
for node in nodes:
    if node['$id'] in dicts and dicts[node['$id']] == 0:
        start_nodes.append(node)
start_options = ["Unity.VisualScripting.Start", "Unity.VisualScripting.Update", "Unity.VisualScripting.OnFixedUpdate"]
filtered_start_nodes = []
for node in start_nodes:
    if node["$type"] in start_options:
        filtered_start_nodes.append(node)
        start_options.remove(node["$type"])

# Different starting point(different sub graph)
for node in filtered_start_nodes:
    new_node = graph_dict[node["$id"]]
    graph = CFG_Graph()
    entry_node = graph.add_node("Entry")
    recursive(new_node, graph, entry_node)

    graph.print_graph()
    

