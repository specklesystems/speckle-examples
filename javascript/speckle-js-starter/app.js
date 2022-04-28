import { getStreamCommits } from "./speckleUtils.js"
const jv = window["w-jsonview-tree"]

let token = sessionStorage.getItem("auth-token")
let streamId = sessionStorage.getItem("stream-id")
let stream = null
let mainBranch = null

let objUrl = (streamId, objId) =>
  `https://speckle.xyz/streams/${streamId}/objects/${objId}`

// Create the viewer instance
let viewer = new window.Speckle.Viewer({
  container: document.getElementById("viewer"),
  showStats: false
})

// Handle selection events
viewer.on("select", objects => {
  console.info(`Selection event. Current selection count: ${objects.length}.`)
  console.log(objects)
  let parent = document.getElementById("selected-panel")
  if (objects.length === 0) {
    parent.classList.add("is-hidden")
    return
  }
  let el = document.getElementById("selection-area")
  jv(objects, el, { expanded: true })
  parent.classList.remove("is-hidden")
})

// Handle double click events
viewer.on("object-doubleclicked", obj => {
  console.info("Object double click event.")
  console.log(obj ? obj : "nothing was doubleckicked.")
})

async function reload() {
  document.getElementById("reloadButton").classList.add("is-loading")

  streamId = document.getElementById("stream-input").value // Get the value of the stream input
  token = document.getElementById("token-input").value // Get the value of the token input
  await fetchStreamData() // Fetch the stream data
  await reloadViewer() // Reload the viewer once the stream data has been fetched

  document.getElementById("reloadButton").classList.remove("is-loading")
}

// Get the stream data and process it
async function fetchStreamData() {
  await getStreamCommits(streamId, 1, null, token).then(str => {
    stream = str.data.stream

    // Split the branches into "main" and everything else
    mainBranch = stream.branches.items.find(b => b.name == "main")
    console.log("main branch", mainBranch)
  })
}

// Reload of the viewer data, remove all objects, reload everything and zoom.
async function reloadViewer() {
  await viewer.unloadAll()
  await viewer.loadObject(
    objUrl(streamId, mainBranch.commits.items[0].referencedObject)
  )
  console.log(`Loaded latest commit from branch "${mainBranch.name}"`)

  viewer.interactions.zoomExtents(0.95, true)
}

var streamInput = document.getElementById("stream-input")
var tokenInput = document.getElementById("token-input")
tokenInput.value = token
streamInput.value = streamId

document.getElementById("reloadButton").addEventListener("click", reload)
document.getElementById("take-screenshot").addEventListener("click", () => {
  console.log("screenshot clickd")
  let data = viewer.interactions.screenshot() // transparent png.

  let pop = window.open()
  pop.document.title = "SpeckleStreamScreenshot"
  pop.document.body.style.backgroundColor = "grey"

  let img = new Image()
  img.src = data
  pop.document.body.appendChild(img)
})

streamInput.addEventListener("input", e => {
  sessionStorage.setItem("stream-id", e.target.value)
})
tokenInput.addEventListener("input", e => {
  sessionStorage.setItem("auth-token", e.target.value)
})
