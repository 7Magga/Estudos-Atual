package com.matbrye.quarkus.rest;
import com.matbrye.quarkus.rest.dto.CreateUserRequest;
import org.jboss.logging.annotations.MessageLogger;

import javax.ws.rs.*;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;

@Path("/users")
@Consumes(MediaType.APPLICATION_JSON)
@Produces(MediaType.APPLICATION_JSON)
public class UserResource {

    @POST
    public Response createUser(CreateUserRequest createUser){
        return Response.ok(createUser).build();
    }

    @GET
    public Response getAllUsers(){
        return Response.ok().build();
    }

}