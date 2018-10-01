class Shelf extends THREE.Group {
    constructor() {
        super();

        this.init();
    }

    init() {

        var selfRef = this;

        var geometry = new THREE.BoxGeometry(0.7, 1.4, 0.7);
        var shelfmaterials = [
            new THREE.MeshLambertMaterial({ map: new THREE.TextureLoader().load("Models/Shelf/ShelfSide.png"), side: THREE.DoubleSide }), //LEFT
            new THREE.MeshLambertMaterial({ map: new THREE.TextureLoader().load("Models/Shelf/ShelfSide.png"), side: THREE.DoubleSide }), //RIGHT
            new THREE.MeshLambertMaterial({ map: new THREE.TextureLoader().load("Models/Shelf/ShelfBotTop.png"), side: THREE.DoubleSide }), //TOP
            new THREE.MeshLambertMaterial({ map: new THREE.TextureLoader().load("Models/Shelf/ShelfBotTop.png"), side: THREE.DoubleSide }), //BOTTOM
            new THREE.MeshLambertMaterial({ map: new THREE.TextureLoader().load("Models/Shelf/ShelfSide.png"), side: THREE.DoubleSide }), //FRONT
            new THREE.MeshLambertMaterial({ map: new THREE.TextureLoader().load("Models/Shelf/ShelfSide.png"), side: THREE.DoubleSide }), //BACK
        ];
        var shelf = new THREE.Mesh(geometry, shelfmaterials);
        shelf.position.y = 0.7;

        var group = new THREE.Group();
        selfRef.add(shelf);
    }
}
