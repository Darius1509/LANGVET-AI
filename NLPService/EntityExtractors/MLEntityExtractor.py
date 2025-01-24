import json
from utils import openai_extract_terms
from EntityExtractors.EntityExtractorBase import EntityExtractor
from FileManager import FileManager
from LeidenClustering import Graph
import utils
#This class implements EntityExtractorBase class
#The algorithm used in this class is to identify veterinary entities with the help of ML.
#For now, we think of using a Transfomer model to identify entities. (GPT-3, BERT, etc.)

class MLEntityExtractor(EntityExtractor):
    terms = None

    def init(self):
        pass

    def load_terms(self, arg):
        self.terms = arg

    def identify_entities(self, text, db_instance):
        json_terms = openai_extract_terms(text)
        data = json.loads(json_terms)
        index = 0
        graph_nodes = {}
        output = {}
        output['terms'] = []
        output['clusters'] = []
        print(data)
        for term in data['terms']:
            term_id = self.terms.get(term['term'])[0]
            if term_id is None:
                term_id = db_instance.insert_term(term, term['description'])
                self.terms[term] = [term_id, term['description']]
            node = {'term_id': term_id, 'context': term['sentence'], 'term_name': term['term'], 'definition': term['description']}
            graph_nodes[index] = node
            output['terms'].append(term_id)
            index += 1

        manager = FileManager()
        manager.generate_document(graph_nodes)
        g = Graph(graph_nodes)

        communities = g.compute_clusters()
        clusters = []
        for community in communities:
            cluster = {}
            cluster['nodes'] = []
            cluster['summary'] = None
            terms_list = []
            for node in community:
                cluster['nodes'].append(graph_nodes[node])
                terms_list.append(graph_nodes[node]['term_name'])
            print(terms_list)
            cluster['summary'] = utils.generate_cluster_description(terms_list)
            clusters.append(cluster)
        output['clusters'] = clusters

        return output
