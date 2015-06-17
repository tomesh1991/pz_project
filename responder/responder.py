import os
from flask import Flask, session, render_template, request, make_response, jsonify

app = Flask(__name__)
app.secret_key = os.urandom(24)
content = ''

@app.route('/api/measurements', methods=['GET', 'POST'])
def print_body():
    global content
    if request.method == 'POST':
        content = request.get_json()
        print(repr(content))
        return jsonify(result={"status": 200})
    if request.method == 'GET':

        return '''
<html>
  <head>
    <title>Home Page</title>
  </head>
  <body>''' + repr(content) + '''
  </body>
</html>
'''


@app.route('/')
def index():
    return

if __name__ == '__main__':
    app.run(
        host="localhost",
        port=int("52123")
    )
