const data = require("./data.json");
const fs = require("fs");

let text = "";
for (const i in data) {
    let a = `{${data[i]["MAP_CODE"]}, "${data[i]["Location_name"]}"},\n\t\t{${data[i]["camp base"]}, "${data[i]["Location_name"]} (Camp Base)"},\n\t\t{${data[i]["area_1"]}, "${data[i]["Location_name"]} (Area 1)"},\n\t\t{${data[i]["area_2"]}, "${data[i]["Location_name"]} (Area 2)"},\n\t\t{${data[i]["area_3"]}, "${data[i]["Location_name"]} (Area 3)"},\n\t\t{${data[i]["area_4"]}, "${data[i]["Location_name"]}  (Area 4)"},\n\t\t{${data[i]["area_5"]}, "${data[i]["Location_name"]} (Area 5)"},\n\t\t{${data[i]["area_6"]}, "${data[i]["Location_name"]} (Area 6)"},\n\t\t{${data[i]["area_7"]}, "${data[i]["Location_name"]} (Area 7)"},\n\t\t{${data[i]["area_8"]}, "${data[i]["Location_name"]} (Area 8)"},\n\t\t{${data[i]["area_9"]}, "${data[i]["Location_name"]} (Area 9)"},\n\t\t{${data[i]["area_10"]}, "${data[i]["Location_name"]} (Area 10)"},\n\t\t{${data[i]["area_11"]}, "${data[i]["Location_name"]} (Area 11)"},\n\t\t{${data[i]["area_12"]}, "${data[i]["Location_name"]} (Area 12)"}`;
    let b = `{\n\t${data[i]["MAP_CODE"]},\n\tnew Dictionary<int, string>\n\t{\n\t\t${a}\n\t}\n},\n`
    text += b;
}

fs.writeFileSync("./text.txt", text);