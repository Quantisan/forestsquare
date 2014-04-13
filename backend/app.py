from bottle import Bottle, run, request, response

app = Bottle()

# Should use a POST but easier for iOS app to send GET ... so whatever
@app.get('/<treeId>')
def get_create_tree(treeId):
    """
    """
    row = ss.next_append_row(worksheet, col)
    if row is not None:
        response.status = '200 OK'
        return {'key': worksheet,
                'col': col,
                'row': row}
    else:
        response.status = '500 Internal Server Error'
        return response

