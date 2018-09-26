class Robot extends THREE.Group {
    constructor() {
        super();

        this.init();
    }

    init() {

        var selfRef = this;

        var geometry = new THREE.BoxGeometry(0.9, 0.3, 0.9);
        var robotmaterials = [
            new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("Models/Robot/robot_side.png"), side: THREE.DoubleSide }), //LEFT
            new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("Models/Robot/robot_side.png"), side: THREE.DoubleSide }), //RIGHT
            new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("Models/Robot/robot_top.png"), side: THREE.DoubleSide }), //TOP
            new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("Models/Robot/robot_bottom.png"), side: THREE.DoubleSide }), //BOTTOM
            new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("Models/Robot/robot_front.png"), side: THREE.DoubleSide }), //FRONT
            new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("Models/Robot/robot_front.png"), side: THREE.DoubleSide }), //BACK
        ];
        var robot = new THREE.Mesh(geometry, robotmaterials);
        robot.position.y = 0.15;
        robot.name = "robot";
        robot.userData = { status: "idle" };

        var group = new THREE.Group();
        selfRef.add(robot);
    }
}
