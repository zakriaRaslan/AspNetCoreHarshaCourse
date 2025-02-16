document.querySelector("#display-friends-list").addEventListener("click", async function () {

    var responseBody = await (await fetch("display-friends-list")).text();
    document.querySelector("#friends-list").innerHTML = responseBody
})