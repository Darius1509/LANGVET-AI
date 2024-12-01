from flask import Flask, request, jsonify
from EntityExtractors.DictionaryEntityExtractor import CorpusEntityExtractor
from EntityExtractors.MLEntityExtractor import MLEntityExtractor
from DatabaseManager import DBManager
from dotenv import load_dotenv
import os
from flask_cors import CORS

app = Flask(__name__)
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

    extractor = CorpusEntityExtractor()
    extractor.load_onto('ontologies/vsao.owl')
    text = request.args.get('text')
    return jsonify({"terms": extractor.identify_entities(text, db_instance)})


if __name__=='__main__':

    load_dotenv()
    db_instance = DBManager(dbname=os.getenv('dbname'), user=os.getenv('user'), password=os.getenv('password'), host=os.getenv('host'))

    app.run(debug=True)

