export const userInfoQuery = () => `query {
      user {
        name
      },
      serverInfo {
        name
        company
      }
    }`

export const streamCommitsQuery = (streamId, itemsPerPage, cursor) => `query {
  stream(id: "${streamId}"){
    branches {
      totalCount
      cursor
      items {
        name
        commits(limit: 1){
          items {
            referencedObject
          }
        }
      }
    }
  }
}`

export const streamSearchQuery = search => `query {
      streams(query: "${search}") {
        totalCount
        cursor
        items {
          id
          name
          updatedAt
        }
      }
    }`
