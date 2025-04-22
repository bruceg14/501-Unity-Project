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

