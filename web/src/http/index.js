import Vue from 'vue'
import VueResource from 'vue-resource'
import Configuration from '@/configuration'

Vue.use(VueResource)

const http = Vue.http

http.options.root = Configuration.value('VUE_APP_BACKEND_URL')

export { http }
