from pdfme import PDF
import os
import shutil
from bing_image_downloader import downloader

def add_page(pdf, term):
    base_path = f"C:\\Facultate\\LANGVET-AI-main\\NLPService\\images\\{term['term_name']}\\"
    downloader.download(f"{term['term_name']}", limit=1,  output_dir='images', adult_filter_off=True, force_replace=False, timeout=60, verbose=False)
    final_path = "C:\\Facultate\\LANGVET-AI-main\\NLPService\\fallback.png"
    if os.listdir(base_path) is not []:
        final_path = base_path + os.listdir(base_path)[0]
    pdf.add_page()
    pdf._text({
    '.': [f"{term['term_name']}"],
    'style': {
        'b': True,
        'i': True,
        'u': True,
        's': 20,
        'f': 'Courier',
        'c': 0.1,
        'r': 0.5,
        'margin_bottom': 400,
    },
    'label': 'a_important_paragraph',
    'uri': f"https://www.dictionary.com/browse/{term['term_name']}",
})

    pdf._content({"content": [{"image": f"{final_path}", 'style': { 'image_place': 'flow', 'margin_left': 100, 'margin_right': 100}}, 
                          {".": f"{term['definition']}", "style": {"f":"Courier", "text_align":"j", "margin_top":10}},
                          {".":"Context: ", "style": {"margin_top":20}},
                          f"{term['context']}"]})

class FileManager:
    pdf = None

    def __init__(self):
        self.pdf = PDF(page_numbering_style="arabic")

    def generate_document(self, terms):
        for term in terms.values():
            add_page(self.pdf, term)

        with open('output.pdf', 'wb') as f:
            self.pdf.output(f)
        shutil.rmtree('C:\\Facultate\\LANGVET-AI-main\\NLPService\\images')