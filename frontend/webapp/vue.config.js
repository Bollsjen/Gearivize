const { defineConfig } = require('@vue/cli-service')
module.exports = defineConfig({
    transpileDependencies: true,
    devServer: {
        port: 8081,
        proxy: {
            '/api': {
                target: 'https://192.168.150.70:5000',
                ws: true,
                changeOrigin: true
            }
        }

    }
})
