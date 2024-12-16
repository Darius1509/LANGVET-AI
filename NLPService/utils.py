import re
import os
import math
import nltk
from openai import OpenAI
from dotenv import load_dotenv
from collections import Counter
from nltk.tokenize import word_tokenize
from nltk.stem import WordNetLemmatizer
nltk.download('punkt_tab')
nltk.download('wordnet')

CONST_REGEX_WORD = re.compile(r"\w+")

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

def sentences_preprocessor(text: str) -> str:
    lemmatizer = WordNetLemmatizer()
    sentences = nltk.sent_tokenize(text)
    preprocessed_sentences = []
    for sentence in sentences:
        sentence = sentence.lower()
        sentence = re.sub(r'[^a-z0-9\s]', '', sentence)
        words = word_tokenize(sentence)
        lemmatized_words = [lemmatizer.lemmatize(word) for word in words]

        processed_sentence = ' '.join(lemmatized_words)
        preprocessed_sentences.append(processed_sentence)
    
    return preprocessed_sentences

def count_capital_letters(text: str) -> int:
    return sum(1 for c in text if c.isupper())

def count_special_characters(text: str) -> int:
    return sum(1 for c in text if not c.isalnum() and not c.isspace())

def calculate_similarity(vec1, vec2):
    #https://en.wikipedia.org/wiki/Cosine_similarity

    _set = set(vec1.keys()) & set(vec2.keys())
    dot_p = sum([vec1[x] * vec2[x] for x in _set])

    sum1 = sum([vec1[x] ** 2 for x in list(vec1.keys())])
    sum2 = sum([vec2[x] ** 2 for x in list(vec2.keys())])
    sq_product = math.sqrt(sum1) * math.sqrt(sum2)

    if not sq_product:
        return 0.0
    else:
        return float(dot_p) / sq_product


def text_to_freq(text):
    words = CONST_REGEX_WORD.findall(text)
    return Counter(words)

def generate_cluster_description(terms):
    load_dotenv()
    client = OpenAI(api_key=os.getenv('openai_key'))
    response = client.chat.completions.create(
    model="gpt-3.5-turbo-0125",
    messages=[
        {
        "role": "system",
        "content": [
            {
            "text": "Your task is to take a list of terms, separated by commas, that are related to the veterinary domain and generate a short descriptive summary, explaining their common themes and significance.",
            "type": "text"
            }
        ]
        },
        {
        "role": "user",
        "content": [
            {
            "text": ', '.join(terms),
            "type": "text"
            }
        ]
        }
    ])
    return response.choices[0].message.content


def openai_extract_terms(text):
    load_dotenv()
    client = OpenAI(api_key=os.getenv('openai_key'))
    response = client.chat.completions.create(
    model="gpt-3.5-turbo-0125",
    messages=[
        {
        "role": "system",
        "content": [
            {
            "text": "Your task is to extract the veterinary medical terms from a given text. Your output will be a json string that contains the list with the terms. Each entry in the list will be a term, the sentence where the term was found and a short description of the term. Please output only the json and don't enclose the output in '```'. Try that with the following text:",
            "type": "text"
            }
        ]
        },
        {
        "role": "user",
        "content": [
            {
            "text": text,
            "type": "text"
            }
        ]
        }
    ])
    return response.choices[0].message.content


#openai_extract_terms("Within the limb, the metapodial skeleton plays a key role in connecting the main body to the digits. The femur, part of the hindlimb, is one of the largest bones in vertebrates and exemplifies the role of mineralized bone tissue in weight-bearing.")
