

export function uptimegraph(parent, dense) {
    var div = document.createElement("div");
    var data = JSON.parse(parent.dataset.timedata);
    var widthModifier = 0;
    if (dense) {
        widthModifier = 4;
    }
    var boxWidth = (10 - widthModifier) * 90;
    div.innerHTML = '<svg style="width:100%; height: 32px;"  xmlns="http://www.w3.org/2000/svg" preserveAspectRatio="xMinYMid slice" version="1.1" viewBox="0 0 ' + boxWidth + ' 32">';
    var svg1 = div.firstChild;
    for(var a = 0; a < 90; a++) {
        var rect = document.createElementNS("http://www.w3.org/2000/svg", "rect");
        var attributes = {
            height: 32,
            width: 8 - widthModifier,
            x: (10 - widthModifier) * a,
            y: 0,
            fill: "#18f264",
            "fill-opacity": 1,
            rx: 4,
            ry: 4,
            "aria-expanded": false,
            title: ""
        };
        if(data[a] < 1 && data[a] >= 0.9) {
            attributes["fill"] = "#f1f10d";
        }
        if (data[a] < 0.75 && data[a] >= 0) {
            attributes["fill"] = "#f10d10";
        }
        if(data[a] < 0) {
            attributes["fill"] = "#bebebe";
        }
        for (var key in attributes) {
            rect.setAttribute(key, attributes[key]);
        }

        svg1.appendChild(rect);
    }
    parent.appendChild(svg1);
}

export function attachuptimegraphtoall(dense) {
    const elementsWithTimeData = document.querySelectorAll('[data-timedata]');

    elementsWithTimeData.forEach(el => {
        if (el.children.length === 0) {
            uptimegraph(el, dense);
        }
    });
}
