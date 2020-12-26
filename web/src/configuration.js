import dotenv from 'dotenv'

dotenv.config()
process.env.TZ = 'America/Sao_Paulo'

export default class Configuration {
  static get CONFIG () {
    return {
      NODE_ENV: '$NODE_ENV',
      VUE_APP_BACKEND_URL: '$VUE_APP_BACKEND_URL',
      TZ: 'America/Sao_Paulo'
    }
  }

  static value (name) {
    if (!(name in this.CONFIG)) {
      console.log(`Configuration: There is no key named ${name}`)
      return undefined
    }

    const value = this.CONFIG[name]

    if (!value) {
      console.log(`Configuration: Value for ${name} is not defined`)
      return undefined
    }

    if (value.startsWith('$VUE_APP_') || value === '$NODE_ENV') {
      const envName = value.substr(1)
      const envValue = process.env[envName]

      if (envValue) { return envValue }

      console.log(`Configuration: Environment variable ${envName} is not defined`)
    }
    return value
  }
}
