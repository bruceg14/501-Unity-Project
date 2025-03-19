import sqlite3 
import json

connect = sqlite3.connect('/Volumes/T7 Shield/501Project/ue_blueprint_graphs.db')

cursor = connect.cursor()

print("Opened database successfully")

# Used this part to randomly select 30 function graph
# cursor.execute("SELECT graph_uuid FROM graph WHERE graph_type = 'FunctionGraph' ORDER BY RANDOM() LIMIT 30")
# randomly_selected_graph_uuids = cursor.fetchall()
# print(randomly_selected_graph_uuids)

# Copy the result form the terminal
randomly_selected_graph_uuids = [('36e12de2-780f-4ca8-8f36-78e6f981c788',), ('571b5884-aaed-47a1-9ead-3b03e9ae82f3',), ('5b1a267c-6f68-4a55-89ba-adf5f38e8598',), ('55a9e8fb-ccdd-48be-aee0-b023fc0cbdd5',), ('413b2d0d-52c6-4080-93ed-3429dc220e1a',), ('acbe279d-ae8a-48a2-af6e-c970fde4e9fd',), ('284491ee-c550-4ed1-9b59-55bc54179b71',), ('880a5937-0ffc-47fc-9462-7c869535432e',), ('60147349-590c-4e22-b354-d0e8d0794c50',), ('69392ef7-3088-4a50-b44e-5406b23d8f84',), ('984abaf5-03b1-4f55-a3e2-14036044edf5',), ('fc045bbc-08d5-4406-9237-33c485ae873a',), ('52494ccd-3945-468c-bc99-8859f41dfc7b',), ('ba13ffad-910d-426a-8087-af4e2389c485',), ('2dbc6f27-185e-4812-9da4-a1c0e21dddff',), ('5fe4b394-c44c-4cd4-aa24-539bc6fdf1ca',), ('ce308a1b-9037-4a27-af94-0fb94d785d5d',), ('b2778120-200f-42eb-b813-a745e983e403',), ('e3ce2ea0-7df9-4389-806e-516620e17754',), ('c99c1fc9-6e35-461b-b23c-d63930fb2d88',), ('5977ee82-f992-4531-a2d7-c4e2416f1960',), ('f16f7087-cecc-4d76-a093-a8fdb215df7d',), ('1accf47d-3613-4e9d-a210-a32553c893c5',), ('3cd9315a-2d90-4bd0-b88b-1548858f99a7',), ('3b6a02bf-6591-44bd-a3ee-2c13ec48ac8e',), ('7d57efc1-3a94-4521-abc8-7ca0cee4cce2',), ('a897060c-4802-429c-8e63-1dd20b3ba0db',), ('b55bb63e-7f3b-4eab-9341-d74d94881e9e',), ('03003feb-f9aa-425d-894f-29f5a7e01401',), ('dd53484f-dcc1-4e8b-80d8-641db6437333',)]

dict_graph = {}

for graph in randomly_selected_graph_uuids:
    print(type(graph[0]))
    cursor.execute("SELECT * from pin_linkedto_pin  WHERE graph_uuid = ?", graph)
    rows = cursor.fetchall()
    edges = rows

    cursor.execute("SELECT node_uuid, node_type from node WHERE node_uuid in (SELECT node_uuid from graph_node WHERE graph_uuid = ?)", graph)
    rows = cursor.fetchall()
    nodes = rows

    dict_graph[graph[0]] = {"nodes": nodes, "edges": edges}

with open("blueprint_graphs.json", "w") as json_file:
    json.dump(dict_graph, json_file, indent=4)



connect.close()