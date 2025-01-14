from flask import Flask, request, jsonify
from EntityExtractors.DictionaryEntityExtractor import CorpusEntityExtractor
from EntityExtractors.MLEntityExtractor import MLEntityExtractor
from DatabaseManager import DBManager
from dotenv import load_dotenv
import os
from flask_cors import CORS
import time

app = Flask(__name__)
terms = {}
extractor = None
CORS(app)

def get_extractor(extractor_name):
    if extractor_name == "dictionary":
        return CorpusEntityExtractor()
    elif extractor_name == "ml":
        return MLEntityExtractor()
    else:
        return None


@app.route('/api/get_terms/', methods=['GET'])
def get_terms():

    text = request.args.get('text')
    result = extractor.identify_entities(text, db_instance)
    return jsonify(result)


if __name__=='__main__':

    load_dotenv()
    db_instance = DBManager(dbname=os.getenv('dbname'), user=os.getenv('user'), password=os.getenv('password'), host=os.getenv('host'))

    for term in db_instance.get_terms():
        terms[term[1]] = [term[0], term[2]]

    extractor = CorpusEntityExtractor()
    extractor.load_onto('E:\\vsc\\NLPService\\ontologies\\vsao.owl')
    extractor.load_terms(terms)
    app.run(debug=True)

