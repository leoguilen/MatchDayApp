module.exports = {
  chainWebpack: (config) => {
    config
      .plugin('html')
      .tap((args) => {
        const argZero = args[0]
        argZero.title = 'MatchDayApp'
        return args
      })
  }
}
