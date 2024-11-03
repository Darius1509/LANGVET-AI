import unittest
import utils

class TestPreprocessor(unittest.TestCase):
    def test_preprocessor_empty(self):
        text_after_preprocessing = utils.preprocessor("")
        self.assertEqual(text_after_preprocessing, "")

    def test_preprocessor_no_capital_letters(self):
        text_after_preprocessing = utils.preprocessor("Text foR verifying the capital LETTERS cOuNt")
        result = utils.count_capital_letters(text_after_preprocessing)
        self.assertEqual(result, 0)

    def test_preprocessor_no_special_characters(self):
        text_after_preprocessing = utils.preprocessor("Text: for,! veri[fying speci@l char3acters!!!\")")
        result = utils.count_special_characters(text_after_preprocessing)
        self.assertEqual(result, 0)

if __name__ == '__main__':
    unittest.main()