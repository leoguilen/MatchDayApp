import { createApp } from 'vue'
import App from './App'
import router from './router'
import store from './store'

import '@fortawesome/fontawesome-free/css/all.css'
import '@fortawesome/fontawesome-free/js/all.js'
import './styles/main.scss'

createApp(App)
  .use(store)
  .use(router)
  .mount('#app')
