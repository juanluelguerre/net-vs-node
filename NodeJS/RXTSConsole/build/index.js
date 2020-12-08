"use strict";
var __assign = (this && this.__assign) || function () {
    __assign = Object.assign || function(t) {
        for (var s, i = 1, n = arguments.length; i < n; i++) {
            s = arguments[i];
            for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p))
                t[p] = s[p];
        }
        return t;
    };
    return __assign.apply(this, arguments);
};
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [op[0] & 2, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};
Object.defineProperty(exports, "__esModule", { value: true });
var Foo_1 = require("./Foo");
var service_1 = require("./service");
var operators_1 = require("rxjs/operators");
var foo = new Foo_1.Foo();
foo.id = 42;
foo.is_verified = true;
var id = foo.id;
console.log("" + id);
// LESSON 1) - PROMISES
// Just return an users collections
service_1.getUsersAsync().then(function (users) {
    console.log((users === null || users === void 0 ? void 0 : users.total) + " users found !");
    users === null || users === void 0 ? void 0 : users.data.forEach(function (u) {
        console.log("   . " + u.first_name + " " + u.last_name + " - " + u.email);
    });
});
// LESSON 2) - ASYNC & AWAIT
// Just return an users collections using async & await
function runUsersAsync() {
    return __awaiter(this, void 0, void 0, function () {
        var users;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0: return [4 /*yield*/, service_1.getUsersAsync()];
                case 1:
                    users = _a.sent();
                    console.log((users === null || users === void 0 ? void 0 : users.total) + " users found !");
                    users === null || users === void 0 ? void 0 : users.data.forEach(function (u) {
                        console.log("   - " + u.first_name + " " + u.last_name + " - " + u.email);
                    });
                    return [2 /*return*/];
            }
        });
    });
}
runUsersAsync();
// LESSON 3) - OBSERVABLES
service_1.getSampleAsync().then(function (data) {
    data.subscribe(function (s) {
        console.log("- " + s);
    });
});
// Return an observable users collections
service_1.getObservableUsersAsync().then(function (data) {
    data.subscribe(function (u) { return console.log("   * " + u.first_name + " " + u.last_name + " - " + u.email); }, function (error) { return console.log("error: " + error); }, function () {
        /* console.log('No more user found !') */
    });
});
function runObservableUsersAsync() {
    return __awaiter(this, void 0, void 0, function () {
        var data;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0: return [4 /*yield*/, service_1.getObservableUsersAsync()];
                case 1:
                    data = _a.sent();
                    data.subscribe(function (u) { return console.log("   # " + u.first_name + " " + u.last_name + " - " + u.email); }, function (error) { return console.log("error: " + error); }, function () {
                        /* console.log('No more user found !') */
                    });
                    return [2 /*return*/];
            }
        });
    });
}
runObservableUsersAsync();
// LESSON 5) - Pipes
// 'pipe', is used to add operatros: filter, map, scan, reduce, ...
// Operators samples: https://www.learnrxjs.io/learn-rxjs/operators
service_1.getObservableUsersAsync().then(function (users) {
    users
        .pipe(operators_1.filter(function (u) { return u.last_name.startsWith("F"); }), operators_1.map(function (u) { return (__assign(__assign({}, u), { email: u.email.replace("@reqres.in", "@rxjs.dev") })); }))
        .subscribe(function (u) { return console.log("   - " + u.first_name + " " + u.last_name + " - " + u.email); }, function (err) { return console.log("error: " + err); }, function () {
        /*  console.log('No more user found !') */
    })
        .unsubscribe();
});
function runObservableUsers_with_pipe_Async() {
    return __awaiter(this, void 0, void 0, function () {
        var users;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0: return [4 /*yield*/, service_1.getObservableUsersAsync()];
                case 1:
                    users = _a.sent();
                    users
                        .pipe(operators_1.filter(function (u) { return u.last_name.startsWith("F"); }), operators_1.map(function (u) { return (__assign(__assign({}, u), { email: u.email.replace("@reqres.in", "@rxjs.dev") })); }))
                        .subscribe(function (u) { return console.log("   - " + u.first_name + " " + u.last_name + " - " + u.email); }, function (error) { return console.log("error: " + error); }, function () {
                        /*  console.log('No more user found !') */
                    })
                        .unsubscribe();
                    return [2 /*return*/];
            }
        });
    });
}
runObservableUsers_with_pipe_Async();
