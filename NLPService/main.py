from flask import Flask, request, jsonify
from EntityExtractors.DictionaryEntityExtractor import DictionaryEntityExtractor
from EntityExtractors.MLEntityExtractor import MLEntityExtractor

app = Flask(__name__)

def get_extractor(extractor_name):
    if extractor_name == "dictionary":
        return DictionaryEntityExtractor()
    elif extractor_name == "ml":
        return MLEntityExtractor()
    else:
        return None


@app.route('/api/get_terms/', methods=['GET'])
def get_terms():
    extractor_name = request.args.get('extractor')
    extractor = get_extractor(extractor_name)

    if extractor is None:
        return jsonify({"error": "Provided extractor is not a valid one!!"})
    dummy_text = request.args.get('text')

    return jsonify({"entities": extractor.identify_entities()})

if __name__=='__main__':
    app.run(debug=True)