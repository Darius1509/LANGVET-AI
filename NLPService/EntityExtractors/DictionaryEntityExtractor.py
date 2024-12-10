from EntityExtractors.EntityExtractorBase import EntityExtractor
from owlready2 import *
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
        terms = []

        preprocessed_sentences = utils.sentences_preprocessor(text)

        for sentence in preprocessed_sentences:
            for cls in self.ontology.classes():
                if cls.label is not None and cls.label[0] in sentence:
                    term = cls.label[0]
                    definition = cls.IAO_0000115[0] if cls.IAO_0000115 else "No definition"
                    terms.append(db_instance.check_term(term, definition))

        '''preprocessed_text = utils.preprocessor(text)
        for cls in self.ontology.classes():
            term = cls.label[0] if cls.label else "No label"
            definition = cls.IAO_0000115[0] if cls.IAO_0000115 else "No definition"
            if cls.label and term in preprocessed_text:
                terms.append(db_instance.check_term(term, definition))'''

        return terms
    
