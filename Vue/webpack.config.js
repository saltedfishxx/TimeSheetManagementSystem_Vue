var path = require('path');
var HtmlWebpackPlugin = require('html-webpack-plugin');
require("babel-polyfill");

module.exports = {
    mode: 'development',
    resolve: {
        extensions: ['.js', '.vue'],

    },
   
    module: {
         // apply loaders to files that meet given conditions
        rules: [
            {
                test: /\.vue?$/,
                exclude: /(node_modules)/,
                use: 'vue-loader'
            },
            {
                test: /\.js?$/,
                exclude: /(node_modules)/,
                use: 'babel-loader'
            },
            {
                test: /\.(gif|svg|jpg|png)$/,
                loader: "file-loader",
              },
        ],
       
    },
    plugins: [new HtmlWebpackPlugin({
        template: './src/index.html',
        favicon: "./src/static/img/icons/favicon.ico"
    })],
    devServer: {
        historyApiFallback: true
    },
    externals: {
        // global app config object
        config: JSON.stringify({
            apiUrl: 'http://localhost:5000'
        })
    }
}