import psycopg2
from psycopg2 import OperationalError
import uuid

class DBManager:
    _instance = None

    def __new__(cls, *args, **kwargs):
        if cls._instance is None:
            cls._instance = super().__new__(cls)
            cls._instance._initialize_connection(*args, **kwargs)
        return cls._instance

    def insert_term(self, term, definition):
        if self.cursor:
            try:
                self.cursor.execute('SELECT "HighlightedTermId" FROM "highlightedTerms" WHERE "termName" = %s;', (term,))
                result = self.cursor.fetchone()
                if result:
                    return result[0]
                else:
                    random_uuid = str(uuid.uuid4())
                    self.cursor.execute(
                        'INSERT INTO "highlightedTerms" ("HighlightedTermId", "termName", "termDefinition", "termDescription", "termLink", "termSubCluster") VALUES (%s, %s, %s, %s, %s, %s) RETURNING "HighlightedTermId";', (random_uuid, term, definition, "n/a", "n/a", "n/a",)
                    )
                    new_id = self.cursor.fetchone()[0]
                    self.connection.commit() 
                    return new_id
            except Exception as e:
                print(f"Error executing query: {e}")
        else:
            print("No connection to the db.")

    def get_terms(self):
        if self.cursor:
            try:
                self.cursor.execute('SELECT * FROM "highlightedTerms"')
                terms = self.cursor.fetchall()
                return terms
            except Exception as e:
                print(f"Error executing query: {e}")
        else:
            print("No connection to the db.")

    def _initialize_connection(self, dbname, user, password, host, port='5432'):
        try:
            self.connection = psycopg2.connect(
                dbname=dbname,
                user=user,
                password=password,
                host=host,
                port=port
            )
            self.cursor = self.connection.cursor()
            print("Connected to NEONDB")
        except OperationalError as e:
            print(f"Error while connecting to db: {e}")
            self.connection = None
            self.cursor = None
