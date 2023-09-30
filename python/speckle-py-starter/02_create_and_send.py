# Running this script will pull the latest commit from the main branch
# of the specified stream and duplicate it inside a different branch.
# (branch should exist already or the script will fail

from specklepy.api.client import SpeckleClient
from specklepy.api.credentials import get_default_account, get_local_accounts
from specklepy.transports.server import ServerTransport
from specklepy.api import operations
from specklepy.objects.base import Base
from specklepy.objects.geometry import Point
# The id of the stream to work with (we're assuming it already exists in your default account's server)
streamId = "4a4299b11f"
branchName = "duplicated"


# Initialise the Speckle client pointing to your specific server.
client = SpeckleClient(host="https://speckle.xyz")

# Get the default account
account = get_default_account()
# If you have more than one account, or the account is not the default, use get_local_accounts
# accounts = get_local_accounts()
# account = accounts[0]

# Authenticate using the account token
client.authenticate(token=account.token)
# Get the main branch with it's latest commit reference
branch = client.branch.get(streamId, "main", 1)


# Create the server transport for the specified stream.
transport = ServerTransport(client=client, stream_id=streamId)


# TODO: Perform some operation on the received data
newObj = Base()

newObj["myTextProp"] = "Some text value"
newObj["myNumberProp"] = 120003
newObj["mySpeckleProp"] = Point.from_coords(1, 1, 1)


# Send the points using a specific transport
newHash = operations.send(base=newObj, transports=[transport])

# you can now create a commit on your stream with this object
commit_id = client.commit.create(
    stream_id=streamId,
    branch_name=branchName,
    object_id=newHash,
    message="Automatic commit created the python starter example",
    source_application="PyStarter"
)

print("Successfully created commit with id: ", commit_id)
