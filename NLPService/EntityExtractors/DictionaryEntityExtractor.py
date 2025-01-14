from EntityExtractors.EntityExtractorBase import EntityExtractor
from owlready2 import *
from LeidenClustering import Graph
import utils
#This class implements EntityExtractorBase class
#The algorithm used in this class is to identify veterinary entities using a corpus.

class CorpusEntityExtractor(EntityExtractor):

    ontology = None
    terms = None

    def __init__(self):
        pass
    
    def load_onto(self, onto_path):
        self.ontology = get_ontology(onto_path).load()

    def load_terms(self, arg):
        self.terms = arg

    def identify_entities(self, text, db_instance):
        index = 0
        graph_nodes = {}
        output = {}
        output['terms'] = []
        output['clusters'] = []
        preprocessed_sentences = utils.sentences_preprocessor(text)

        for sentence in preprocessed_sentences:
            for cls in self.ontology.classes():
                if cls.label is not None and cls.label[0] in sentence:
                    term = cls.label[0]
                    definition = cls.IAO_0000115[0] if cls.IAO_0000115 else "No definition"
                    term_id = self.terms.get(term)[0]
                    if term_id is None:
                        term_id = db_instance.insert_term(term, definition)
                        self.terms[term] = [term_id, definition]
                    node = {'term_id': term_id, 'context': sentence, 'term_name': term}
                    graph_nodes[index] = node
                    output['terms'].append(term_id)
                    index += 1
        g = Graph(graph_nodes)

        communities = g.compute_clusters()
        clusters = []
        for community in communities:
            cluster = {}
            cluster['nodes'] = []
            cluster['summary'] = None
            for node in community:
                cluster['nodes'].append(graph_nodes[node])
            terms_list = [entry['term_name'] for entry in graph_nodes.values()]
            cluster['summary'] = utils.generate_cluster_description(terms_list)
            clusters.append(cluster)
        output['clusters'] = clusters

        return output
    
