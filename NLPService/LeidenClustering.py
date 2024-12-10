#metric function will be used to evaluate the quality of the partition
#context similarity
import networkx as nx
import matplotlib.pyplot as plt
import random

def metric(graph, communities, node, target_community):
    
    m = graph.size(weight='weight')
    k_i = sum([graph[node][neighbor]['weight'] for neighbor in graph.neighbors(node)])
    gain = 0
    for neighbor in graph.neighbors(node):
        weight_ij = graph[node][neighbor]['weight']
        k_j = sum([graph[neighbor][n]['weight'] for n in graph.neighbors(neighbor)])
        if communities[neighbor] == target_community:
            gain += weight_ij - (k_i * k_j) / (2 * m)
        if communities[neighbor] == communities[node]:
            gain -= weight_ij - (k_i * k_j) / (2 * m)
    
    return gain

class Graph:
    _graph = None

    def __init__(self, list):
        self._graph = nx.Graph()
        self._graph.add_edges_from(list)

    def get_graph(self):
        return self._graph

    def compute_clusters(self, iterations):
        communities = {node: node for node in self._graph.nodes()}

        for _ in range(iterations): 
            nodes = list(self._graph.nodes())
            random.shuffle(nodes)
            for node in nodes:
                best_community = communities[node]
                max_gain = 0
                for neighbor in self._graph.neighbors(node):
                    target_community = communities[neighbor]
                    gain = metric(self._graph, communities, node, target_community)
                    if gain > max_gain:
                        best_community = target_community
                        max_gain = gain
                communities[node] = best_community
