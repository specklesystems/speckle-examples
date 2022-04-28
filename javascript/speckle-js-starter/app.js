import { getStreamCommits, getUserData } from "./speckleUtils.js"

let token = null
let streamId = null
let stream = null
let mainBranch = null
let beams = null
let columns = null
let selectedBeam = null
let selectedColumn = null
let isDropdownLoaded = false

document.getElementById("reloadButton").addEventListener("click", reload)

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
  let el = document.getElementById("selection-area")
  let jv = window["w-jsonview-tree"]
  jv(objects, el, { expanded: false })
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
  if (!isDropdownLoaded) {
    populateDropdown("alternative-selector", beams)
    populateDropdown("alternative-selector-2", columns) // If  it's the first time, populate the dropdown too.
  }
  await reloadViewer() // Reload the viewer once the stream data has been fetched
  document.getElementById("reloadButton").classList.remove("is-loading")
}

// This will add an option for each branch in the alternatives list, and bind the referenced object id as the value
function populateDropdown(id, data) {
  let select = document.getElementById(id)
  data.forEach(alt => {
    if (alt.commits.items.length == 0) return
    var opt = document.createElement("option")
    opt.appendChild(document.createTextNode(alt.name))
    opt.value = alt.commits.items[0].referencedObject
    select.appendChild(opt)
  })
  isDropdownLoaded = true
}

// Get the stream data and process it
//
async function fetchStreamData() {
  await getStreamCommits(streamId, 1, null, token).then(str => {
    stream = str.data.stream

    // Split the branches into "main" and everything else
    mainBranch = stream.branches.items.find(b => b.name == "main")
    beams = stream.branches.items.filter(b => b.name.startsWith("beam"))
    columns = stream.branches.items.filter(b => b.name.startsWith("column"))
    console.log("main branch", mainBranch)
    console.log("beam options", beams)
    console.log("column options", columns)
  })
}

// Reload of the viewer data, remove all objects, reload everything and zoom.
async function reloadViewer() {
  viewer.sceneManager.removeAllObjects()
  await viewer.loadObject(
    objUrl(streamId, mainBranch.commits.items[0].referencedObject)
  )
  console.log(`Loaded latest commit from branch "${mainBranch.name}"`)

  // Load beam if selected
  if (selectedBeam) {
    console.log("alternative is selected, should load", selectedBeam)
    await viewer.loadObject(objUrl(streamId, selectedBeam))
    console.log("loaded alternative")
  }

  // Load column if selected
  if (selectedColumn) {
    console.log("alternative is selected, should load", selectedColumn)
    await viewer.loadObject(objUrl(streamId, selectedColumn))
    console.log("loaded alternative")
  }

  viewer.interactions.zoomExtents(0.95, true)
}

// Reload viewer every time an alternative is selected in the dropdown.
async function onColumnAlternativeSelected(event) {
  const selection = event.target.value.substring(
    event.target.selectionStart,
    event.target.selectionEnd
  )
  selectedColumn = selection
  console.log("selected", selectedColumn)
  document.getElementById("reloadButton").classList.add("is-loading")
  await reloadViewer()
  document.getElementById("reloadButton").classList.remove("is-loading")
}

async function onBeamAlternativeSelected(event) {
  const selection = event.target.value.substring(
    event.target.selectionStart,
    event.target.selectionEnd
  )
  selectedBeam = selection
  console.log("selected", selectedBeam)
  document.getElementById("reloadButton").classList.add("is-loading")
  await reloadViewer()
  document.getElementById("reloadButton").classList.remove("is-loading")
}

// Hack to prevent scene from loosing current zoom.
viewer.sceneManager._postLoadFunction = () => {
  viewer.reflectionsNeedUpdate = true
}

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
