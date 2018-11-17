package com.application.controller;

import com.application.pojo.User;
import com.application.services.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/user")
public class UserController {

    @Autowired
    private UserService userService;

    @RequestMapping(value = "/test", method = RequestMethod.GET)
    public String test() {
        return "Test";
    }

    @RequestMapping(value = "/addUser", method = RequestMethod.POST)
    public User addUser(@RequestParam String userId,
                        @RequestParam String password,
                        @RequestParam Integer levelOfExpertise) {
        return userService.addUser(userId, password, levelOfExpertise);
    }

    @RequestMapping(value = "/{userId}", method = RequestMethod.GET)
    public User getUser(@PathVariable("userId") String userId) {
        System.out.println(userId);
        return userService.getUser(userId);
    }

    @RequestMapping(value = "/checkIfValidCredentials", method = RequestMethod.GET)
    public Boolean checkIfValidCredentials(@RequestParam String userId,
                                           @RequestParam String password) {
        return userService.checkIfValidCredentials(userId, password);
    }

}
