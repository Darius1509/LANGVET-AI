from abc import abstractmethod, ABC


class EntityExtractor(ABC):
    def __init__(self):
        pass

    @abstractmethod
    def identify_entities(self, text):
        #Base method to identify entities. this will be implemented by every extractor we have
        pass
