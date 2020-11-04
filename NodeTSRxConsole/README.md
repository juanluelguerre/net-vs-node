# Typescript + NodeJS

1. Run commands to install dependencies:

```
npm init -y
npm install typescript --save-dev
npm install @types/node --save-dev
```

2. Initializa Typescript to create 'tsconfig.json':
`npx tsc --init --rootDir src --outDir build --esModuleInterop --resolveJsonModule --lib es6 --module commonjs --allowJs true --noImplicitAny true`

3. Intall nodemon:
`npm install --save-dev ts-node nodemon`


4. Add nodemon.json
```
{
  "watch": ["src"],
  "ext": ".ts,.js",
  "ignore": [],
  "exec": "ts-node ./src/index.ts"
}
```

5. Install rimraf
`npm install --save-dev rimraf`

6. add this to the package.json
```
"start:dev": "nodemon",
"build": "rimraf ./build && tsc",`
"start": "npm run build && node build/index.js"

```

## Reactive Programming

`npm install rxjs``
