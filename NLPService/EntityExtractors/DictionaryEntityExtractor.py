from EntityExtractorBase import EntityExtractorBase
from utils import preprocessor
#This class implements EntityExtractorBase class
#The algorithm used in this class is to identify veterinary entities using a dictionary.

class DictionaryEntityExtractor(EntityExtractorBase):
    def __init__(self):
        pass
    
    @preprocessor
    def identify_entities(self, text):
        return "Dictionary"