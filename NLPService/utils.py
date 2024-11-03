import re
from nltk.tokenize import word_tokenize
from nltk.stem import WordNetLemmatizer


#This function has the responsability to preprocess the text before extracting the entities.
#decorator
def preprocessor(function: callable) -> callable:
    def preprocess(*args, **kwargs):
        text = kwargs.get('text')
        if text == "":
            raise ValueError("Text is empty")
        return function(text)
    return preprocess


def preprocessor(text: str) -> str:
    lemmatizer = WordNetLemmatizer()
    
    text = text.lower()
    text = re.sub(r'[^a-z0-9\s]', '', text)
    
    words = word_tokenize(text)
    lemmatized_words = [lemmatizer.lemmatize(word) for word in words]

    processed_text = ' '.join(lemmatized_words)
    
    return processed_text

def count_capital_letters(text: str) -> int:
    return sum(1 for c in text if c.isupper())

def count_special_characters(text: str) -> int:
    return sum(1 for c in text if not c.isalnum() and not c.isspace())