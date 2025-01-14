#metric function will be used to evaluate the quality of the partition
#context similarity
import networkx as nx
import matplotlib.pyplot as plt
import random
from cdlib import algorithms
import utils as u
from itertools import combinations

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

    def __init__(self, terms):
        self._graph = nx.Graph()
        
        entries = list(terms.items())
        entries_pairs = list(combinations(entries, 2))
        for pair in entries_pairs:
            freq_1 = u.text_to_freq(pair[0][1]['context'])
            freq_2 = u.text_to_freq(pair[1][1]['context'])
            similarity = u.calculate_similarity(freq_1, freq_2)
            if similarity > 0.3:
                self._graph.add_edge(pair[0][0], pair[1][0], weight=similarity)

    def get_graph(self):
        return self._graph

    def compute_clusters(self, iterations=1):
        communities = {node: node for node in self._graph.nodes()}
        coms = algorithms.leiden(self._graph)
        for _ in range(iterations): 
            nodes = list(self._graph.nodes())
            random.shuffle(nodes)
            for node in nodes:
                best_community = communities[node]
                max_gain = 0
                for neighbor in self._graph.neighbors(node):
                    target_community = communities[neighbor]
                    gain = max_gain
                    if gain > max_gain:
                        best_community = target_community
                        max_gain = gain
                communities[node] = best_community
        return coms.communities
