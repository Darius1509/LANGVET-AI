To run this service install:

  pip install owlready2==0.47
  
  pip install --user -U nltk
  
  pip install psycopg2-binary python-dotenv
  
  pip install flask

  !!Make sure to have an .env file in the same directory with the "main.py" with the following content:
  
dbname=*fill_from_db_details*

user=*fill_from_db_details*

password=*fill_from_db_details*

host=*fill_from_db_details*

  ...and then run "python main.py"
