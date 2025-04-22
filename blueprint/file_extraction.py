import sqlite3 
import json

# Database from https://zenodo.org/records/14247703
connect = sqlite3.connect('/Volumes/T7 Shield/501Project/ue_blueprint_graphs.db')

cursor = connect.cursor()

print("Opened database successfully")

# Used this part to randomly select 30 function graph
cursor.execute("SELECT graph_uuid FROM graph WHERE graph_type = 'UbergraphPage' ORDER BY RANDOM() LIMIT 1000")
randomly_selected_graph_uuids = cursor.fetchall()
print(randomly_selected_graph_uuids)

dict_graph = {}

for graph in randomly_selected_graph_uuids:
    cursor.execute("SELECT pin1_uuid, pin2_uuid, pin2_owningnode from pin_linkedto_pin  WHERE graph_uuid = ?", graph)
    rows = cursor.fetchall()
    edges = []
    for pin1_uuid, pin2_uuid, pin2_owningnode in rows:
        cursor.execute("SELECT OwningNode, pin_direction, pin_name from pin WHERE pin_uuid = ?", (pin1_uuid, ))
        node1_uuid, pin_direction, pin_name = cursor.fetchone()
        edges.append((node1_uuid, pin2_owningnode, pin_name, pin_direction))

    cursor.execute("SELECT node_uuid, node_type from node WHERE node_uuid in (SELECT node_uuid from graph_node WHERE graph_uuid = ?)", graph)
    rows = cursor.fetchall()
    nodes = []
    for node_uuid, node_type in rows:
        cursor.execute("SELECT node_pos_x, node_pos_y from node WHERE node_uuid = ?", (node_uuid, ))
        node_pos_x, node_pos_y = cursor.fetchone()
        nodes.append((node_uuid, node_type, node_pos_x, node_pos_y))
    dict_graph[graph[0]] = {"nodes": nodes, "edges": edges}
print("Total number of graphs with branching nodes: ", len(dict_graph))
with open("blueprint_graphs_Uber.json", "w") as json_file:
    json.dump(dict_graph, json_file, indent=4)



connect.close()