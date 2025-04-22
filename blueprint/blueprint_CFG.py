import sys
import os
import json
import networkx as nx
import matplotlib.pyplot as plt
import time

sys.path.append(os.path.join(os.path.dirname(__file__), '.')) 

def calculate_cyclomatic_complexity(nodes, edges):
    num_edges = len(edges)
    num_nodes = len(nodes)
    num_end_nodes = 0
    dicts = {}
    for node in nodes:
        dicts[node.id] = 0
    
    for edge in edges:
        if edge.source in dicts:
            dicts[edge.source] = 1

    for node in nodes:
        if node.id in dicts and dicts[node.id] == 0:
            num_end_nodes += 1

    cyclomatic_complexity = num_edges - num_nodes + 2 * num_end_nodes
    return cyclomatic_complexity

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
            edge_id_s = edge.source.id
            edge_content_source = edge.source.content
            edge_content_s = edge_content_source.replace(".", "_")

            edge_content_distination = edge.target.content
            edge_content_d = edge_content_distination.replace(".", "_")
            edge_id_d = edge.target.id
            print(f"node{edge_id_s}_{edge_content_s} -> node{edge_id_d}_{edge_content_d};")


def recursive(BP_node, graph, last_node): # BP_node is a Node class, graph is CFG_graph class, and last_node is CFG_Node class
    cur_node = graph.add_node(BP_node.get_content())
    graph.add_edge(last_node, cur_node)
    last_node = cur_node
    print("I am looping crazy")
    print("BP_node", BP_node.get_content())
    print("children", BP_node.get_children())
    while(len(BP_node.get_children()) > 0):
        child_nodes = BP_node.get_children()
        if BP_node.get_content() == "IfNode":
            if_node = graph.add_node("If")
            graph.add_edge(last_node, if_node)
            after_if_node = graph.add_node("After_if")
            if len(child_nodes) == 2:
                for child_node in child_nodes:
                    if child_node.get_branch_type() == "if_true":
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
                if child_nodes[0].get_branch_type() == "if_true":
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
        elif BP_node.get_content() == "LoopNode":
            loop_node = graph.add_node("Loop")
            graph.add_edge(last_node, loop_node)
            after_loop_node = graph.add_node("After_loop")
            child_nodes = BP_node.get_children()
            for child_node in child_nodes:
                if child_node.get_branch_type() == "loop_body":
                    body_node = graph.add_node("loop_body")
                    graph.add_edge(loop_node, body_node)
                    after_body_node = recursive(child_node, graph, body_node)
                    graph.add_edge(after_body_node, loop_node)    
                elif child_node.get_branch_type() == "exit_loop":
                    graph.add_edge(loop_node, after_loop_node)
                    BP_node = child_node
                    last_node = after_loop_node
        elif BP_node.get_content() == "SwitchNode":
            switch_node = graph.add_node("Switch")
            graph.add_edge(last_node, switch_node)

            after_switch_node = graph.add_node("After_switch")
            for child_node in child_nodes:
                switch_body_node = graph.add_node("One_switch_case")
                graph.add_edge(switch_node, switch_body_node)
                after_body_node = recursive(child_node, graph, switch_body_node)
                graph.add_edge(after_body_node, after_switch_node)    
            return after_switch_node
        else:  
            print("BP_node.get_content()", BP_node.get_content())
            print("child_nodes", child_nodes)
            print("key", key)
            BP_node = child_nodes[0]
            # print("I am here")
        BP_node = child_nodes[0]    

    return last_node

######################################################################################################################

with open('./blueprint_graphs_Uber.json', 'r') as f:
    blueprint_graph_data = json.load(f)

total_CC = 0
graph_count = 0

for key, item in blueprint_graph_data.items():
    graph_dict = {}

    go_to_next_graph = False

    ret = item # Nodes and edges for a graph

    # ret = blueprint_graph_data["486e2288-0f4a-42ed-929c-7111d26f3c0e"]
    print("Parsing", key)
    nodes = ret["nodes"]
    edges = ret["edges"]

    # knots = set()
    # for node in nodes:
    #     if node[1] == 'Knot':
    #         knots.add(node[0])

    nodes_dict = {node[0] : node[1:4] for node in nodes} # Format for nodes_dict: {node_uuid : (node_type, node_pos_x, node_pos_y)}

    # Filter for None and Knot nodes
    for node in nodes:
        if node[1] == 'Knot':
            go_to_next_graph = True
            break
    for edge in edges:
        if edge[0] is None or edge[1] is None:
            go_to_next_graph = True
            break
        if edge[0] not in nodes_dict or edge[1] not in nodes_dict:
            go_to_next_graph = True
            break
    if go_to_next_graph:
        continue


    # Process the nodes given that the database doesn't autmotically indicate the node type for branching
    for edge in edges:
        source = edge[0]
        destination = edge[1]
        pin1_name = edge[2]
        pin_direction = edge[3]
        # This mean this is a for loop node
        if "LoopBody" in pin1_name:
            # This mean it is a loop but not sure if it is a for loop or while loop
            nodes_dict[source][0] = "LoopNode"
        elif "FirstIndex" in pin1_name: #Now its a for loop
            nodes_dict[source][0] = "LoopNode"
        elif "LastIndex" in pin1_name:
            nodes_dict[source][0] = "LoopNode"
        elif "Index" in pin1_name:
            nodes_dict[source][0] = "LoopNode"
        elif "Completed" in pin1_name:
            nodes_dict[source][0] = "LoopNode"
        # If the node is a while loop
        if "Condition" in pin1_name:
            # print("source", source)
            # print("nodes_dict", nodes_dict)
            nodes_dict[source][0] = "LoopNode"

    

    for node in nodes:
        # print("node", node)
        # print("nodes_dict", nodes_dict)
        if "IfThenElse" in nodes_dict[node[0]][0]:
            nodes_dict[node[0]][0] = "IfNode"
        if "Switch" in nodes_dict[node[0]][0]:
            nodes_dict[node[0]][0] = "SwitchNode"

    # Continue to process the graph by labeling node's branch type
    for edge in edges:
        source = edge[0]
        destination = edge[1]
        pin1_name = edge[2]
        pin_direction = edge[3]
        if pin_direction == "EGPD_Output":
            source_node = nodes_dict[source]
            branch_type = "none"
            # print(source_node)
            if source_node[0] == "LoopNode":
                if "LoopBody" in pin1_name:
                    branch_type = "loop_body"
                elif "FirstIndex" in pin1_name:
                    branch_type = "none_branching"
                elif "LastIndex" in pin1_name:
                    branch_type = "none_branching"
                elif "Index" in pin1_name:
                    branch_type = "none_branching"
                elif "Completed" in pin1_name:
                    branch_type = "exit_loop"
                elif "Condition" in pin1_name:
                    branch_type = "none_branching"
                else:
                    branch_type = "none_branching"

            elif source_node[0] == "IfNode":
                if "then" in pin1_name:
                    branch_type = "if_true"
                elif "else" in pin1_name:
                    branch_type = "if_false"
            elif source_node[0] == "SwitchNode" and  pin_direction == "EGPD_Output":
                branch_type = "switch_output"

            if source not in graph_dict:
                graph_dict[source] = Node(nodes_dict[source][0], "none" ,source)
            
            if destination not in graph_dict:
                graph_dict[destination] = Node(nodes_dict[destination][0], branch_type, destination)
            else:
                if branch_type != "none":
                    graph_dict[destination].branch_type = branch_type

            if branch_type != "none_branching":
                graph_dict[source].add_child(graph_dict[destination])

    # Find the starting node
    # Blueprint has an "Event" node as implicit entry point. I am using that as to generate the CFG
    event_node = None
    for node in nodes:
        if node[1] == 'Event':
            print("Found Event node", node[0])
            event_node = node[0]
            break

    # print("node", event_node)
    if event_node is None:
        print("No Event node found")
        continue

    if event_node not in graph_dict:
        print("Event node not in graph_dict")
        continue
    start_node = graph_dict[event_node] #Event node
    graph = CFG_Graph()
    entry_node = graph.add_node("Entry")
    recursive(start_node, graph, entry_node)
    graph.print_graph()   
    cc_value = calculate_cyclomatic_complexity(graph.nodes, graph.edges)
    print("Cyclomatic Complexity: ", cc_value)
    total_CC += cc_value
    graph_count += 1

print("Total Cyclomatic Complexity: ", total_CC)
print("Number of graphs: ", graph_count)
print("Average Cyclomatic Complexity: ", total_CC / graph_count)
