const { defineConfig } = require('@vue/cli-service')
module.exports = defineConfig({
    transpileDependencies: true,
    devServer: {
        port: 8081,
        proxy: {
            '/api': {
                target: 'https://localhost:8080',
                ws: true,
                cahngeOrigin: true
            }
        }
    }
})
