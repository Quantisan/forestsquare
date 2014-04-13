from bottle import Bottle, run, request, response
import evn
import sys

app = Bottle()

# Should use a POST but easier for iOS app to send GET ... so whatever
@app.get('/tree/<treeId>')
def get_create_tree(treeId):
    try:
        height = request.query.height
        radius = request.query.radius
        locationX = request.query.locationX  ## UTM X-coordinate
        locationY = request.query.locationY
        species = request.query.species

        guid = evn.recordTree(treeId, height, radius, locationX, locationY, species)

        response.status = '200 OK'
        return {'guid': guid}
    except:
        response.status = '500 Internal Server Error'
        print "Unexpected error: ", sys.exc_info()[0]
        return

@app.get('/crossdomain.xml')
def get_crossdomain():
    return """<?xml version="1.0"?>
            <cross-domain-policy>
            <allow-access-from domain="*" />
            </cross-domain-policy>"""

if __name__ == "__main__":
    run(app, host='localhost', port=8080, reloader=True, debug=True)
