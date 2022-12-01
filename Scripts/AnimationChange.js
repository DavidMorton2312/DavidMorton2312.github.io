var firstItem = document.querySelector('.content__left-item1');
var secondItem = document.querySelector('.content__left-item2');

var link1 = firstItem.querySelector('.content__left-link1');
var link2 = secondItem.querySelector('.content__left-link2');

setInterval(function () {
    firstItem.classList.toggle('changeBackground1');
    link1.classList.toggle('fontcolor1');
    secondItem.classList.toggle('changeBackground2');
    link2.classList.toggle('fontcolor2');
}, 3000)