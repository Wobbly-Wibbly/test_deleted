<?php
include 'db.php';
include 'utils.php';
$db = db::getInstance();

$statement = $db->prepare("select * from words");
$statement->execute(array());
$data = $statement->fetchAll();
utils::jsonResponse($data[rand(0,count($data)-1)]);
