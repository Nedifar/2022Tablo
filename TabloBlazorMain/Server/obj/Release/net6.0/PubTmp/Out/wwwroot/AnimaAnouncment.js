export function animationAnnouncment() {
    var elem = document.querySelector('#annoncment');
    var flkty = new Flickity(elem, {
        autoPlay: 10000,
        pauseAutoPlayOnHover: false,
        prevNextButtons: false,
        pageDots: false,
        wrapAround: true
    });
    
}
export function vanila() {
    var elem = document.querySelector('#annoncment');
    var flkty = new Flickity(elem, {
        autoPlay: 10000,
        pauseAutoPlayOnHover: false,
        prevNextButtons: false,
        pageDots: false,
        wrapAround: true
    });

}

export function create() {
    var flick = Flickity.data('#annoncment');
    flick.destroy();
}

export function refresh() {
        setTimeout(function () {
            location.reload();
        }, 10000);
}