import * as Flickity from "flickity";

export function animationAnnouncment() {
    var element = document.getElementById('annoncment');
    while (element.firstChild) {
        element.removeChild(element.firstChild);
    }
    //var elem = document.querySelector('#annoncment');
    //let flkty = new Flickity(elem, {
    //    wrapAround: true,
    //    autoPlay: 1500,
    //    pauseAutoPlayOnHover: false,
    //    prevNextButtons: false,
    //    pageDots: false
    //});
    //window.caca = flkty;
}

export function vanila() {
    window.caca.destroy();
}