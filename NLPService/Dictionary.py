class Dictionary:
    _instance = None

    def __new__(cls):
        if cls._instance is None:
            cls._instance = super(Dictionary, cls).__new__(cls)
            cls._instance.dictionary = {}
        return cls._instance

    def populate_dictionary(self):
        #TODO: Implement this method
        pass

    def get_dictionary(self):
        return self.dictionary

    def check_word(self, word):
        return word in self.dictionary
