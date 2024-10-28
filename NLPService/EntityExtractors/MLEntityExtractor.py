from EntityExtractorBase import EntityExtractorBase
from utils import preprocessor

#This class implements EntityExtractorBase class
#The algorithm used in this class is to identify veterinary entities with the help of ML.
#For now, we think of using a Transfomer model to identify entities. (GPT-3, BERT, etc.)

class MLEntityExtractor(EntityExtractorBase):
    def __init__(self):
        pass

    @preprocessor
    def identify_entities(self):
        return "ML"