from EntityExtractors.EntityExtractorBase import EntityExtractor
from owlready2 import *
from LeidenClustering import Graph
import utils
#This class implements EntityExtractorBase class
#The algorithm used in this class is to identify veterinary entities using a corpus.

class CorpusEntityExtractor(EntityExtractor):

    ontology = None

    def __init__(self):
        pass
    
    def load_onto(self, onto_path):
        self.ontology = get_ontology(onto_path).load()

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
                    term_id = db_instance.check_term(term, definition)
                    test = {'term_id': term_id, 'context': sentence, 'term_name': term}
                    graph_nodes[index] = test
                    output['terms'].append(term_id)
                    index += 1
        
        g = Graph(graph_nodes)

        communities = g.compute_clusters(10)
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
        
        '''preprocessed_text = utils.preprocessor(text)
        for cls in self.ontology.classes():
            term = cls.label[0] if cls.label else "No label"
            definition = cls.IAO_0000115[0] if cls.IAO_0000115 else "No definition"
            if cls.label and term in preprocessed_text:
                terms.append(db_instance.check_term(term, definition))'''

        return output
    
