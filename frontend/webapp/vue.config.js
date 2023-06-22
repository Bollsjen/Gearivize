const { defineConfig } = require('@vue/cli-service')
module.exports = defineConfig({
    transpileDependencies: true,
    devServer: {
        port: 8081,
        proxy: {
            '/api': {
                target: 'https://localhost:3000',
                ws: true,
                changeOrigin: true
            }
        }

    }
})
