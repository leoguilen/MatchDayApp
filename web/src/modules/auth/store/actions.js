import * as types from './mutation-types'

export const ActionSetUser = ({ commit }, payload) => {
  commit(types.SET_USER, payload)
}
