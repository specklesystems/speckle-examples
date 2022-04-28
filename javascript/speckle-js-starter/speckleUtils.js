import {
  streamCommitsQuery,
  streamSearchQuery,
  userInfoQuery
} from "./speckleQueries.js"

export let SERVER_URL = "https://speckle.xyz"

// Calls the GraphQL endpoint of the Speckle server with a specific query.
export async function speckleFetch(query, token) {
  console.log("fetching with token ", token)
  if (token)
    try {
      var res = await fetch(`${SERVER_URL}/graphql`, {
        method: "POST",
        headers: {
          Authorization: "Bearer " + token,
          "Content-Type": "application/json"
        },
        body: JSON.stringify({
          query: query
        })
      })
      return await res.json()
    } catch (err) {
      console.error("API call failed", err)
    }
  else return Promise.reject("You are not logged in (token does not exist)")
}

// Fetch the current user data using the userInfoQuery
export const getUserData = token => speckleFetch(userInfoQuery(), token)

// Fetch for streams matching the specified text using the streamSearchQuery
export const searchStreams = (e, token) => speckleFetch(streamSearchQuery(e))

// Get commits related to a specific stream, allows for pagination by passing a cursor
export const getStreamCommits = (streamId, itemsPerPage, cursor, token) =>
  speckleFetch(streamCommitsQuery(streamId, itemsPerPage, cursor), token)
