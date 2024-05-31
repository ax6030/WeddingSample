const path = require('path');
const fs = require('fs');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const MiniCssExtractPlugin = require("mini-css-extract-plugin");

function getEntries() {
  const jsDir = path.resolve(__dirname, 'src/js');
  const cssDir = path.resolve(__dirname, 'src/css');
  const entries = {};


  fs.readdirSync(cssDir).forEach(file => {
    if (file.endsWith('.css')) {
      const name = path.basename(file, '.css');
      entries[`css/${name}`] = path.resolve(cssDir, file);
    }
  });
  
  
  fs.readdirSync(jsDir).forEach(file => {
    if (file.endsWith('.js')) {
      const name = path.basename(file, '.js');
      entries[`js/${name}`] = path.resolve(jsDir, file);
    }
  });

  return entries;
}

const entries = getEntries();


module.exports = {
  entry: entries,
  output: {
    path: path.resolve(__dirname, 'dist'),
    filename: (pathData) => {
      // 根据入口名称动态设置输出路径
      if (fs.existsSync(entries[pathData.chunk.name])) {
        // 直接返回文件名，不包含路径信息
        return `[name].${entries[pathData.chunk.name].endsWith('.css') ? 'css' : 'js'}`;
      } else {
        return 'js/[name].js'; // 默认 JS 路径
      }
    }
  },
  mode: 'development',
  devServer: {
    static: './dist',
  },
  resolve: {
    modules: [
      path.resolve(__dirname, 'src'),
      'node_modules'
    ]
  },
  module: {
    rules: [
      {
        test: /\.css$/i,
        use: [MiniCssExtractPlugin.loader, "css-loader"],
      },
      {
        test: /\.m?js$/,
        exclude: /node_modules/,
        use: {
          loader: "babel-loader",
        }
      }
    ],
  },
  plugins: [new HtmlWebpackPlugin({
    template: './src/index.html'
  }),
  new MiniCssExtractPlugin({
    filename: 'bundle.[name].css',
  })],
  stats: {
    errorDetails: true
  }
};