var path = require('path');
var HtmlWebpackPlugin = require('html-webpack-plugin');
require("babel-polyfill");

module.exports = {
    mode: 'development',
    resolve: {
        alias: {
            'vue$': 'vue/dist/vue.esm.js',
          },
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
              {
                test: /\.css$/,
                loader:[ 'style-loader', 'css-loader' ]
        }
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