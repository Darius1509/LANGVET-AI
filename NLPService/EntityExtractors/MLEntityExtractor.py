import json
from utils import openai_extract_terms
from EntityExtractors.EntityExtractorBase import EntityExtractor

#This class implements EntityExtractorBase class
#The algorithm used in this class is to identify veterinary entities with the help of ML.
#For now, we think of using a Transfomer model to identify entities. (GPT-3, BERT, etc.)

class MLEntityExtractor(EntityExtractor):
    def init(self):
        pass

    def identify_entities(self, text, db_instance):
        terms = []

        json_terms = openai_extract_terms(text)
        print(json_terms)
        data = json.loads(json_terms)
        for term in data:
            terms.append(db_instance.check_term(term['term'], term['description']))
        return terms
